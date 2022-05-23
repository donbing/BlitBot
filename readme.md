# BlitBot
> Blazor Server app for configuring market charts on a remote display

<img src="docs/images/BlitBot.jpg" height="250">
<img src="docs/images/BlitBot_angle.jpg" height="250">

- uses trading view widget
- config editor web-page allows live updates to the chart view on other devices.
- supports all markets available on trading view
- screen on/off toggle to prevent burn-in
- all your favourite TV indicators are supported
- user selectable timezone 
- timeframe selection
- toggleable symbol details panel
- technical analysis view


## Build/publish 64 bit arm linux package
```sh
# setup .net
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel Current
echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
source ~/.bashrc

# checkout the BlitBot code
git clone https://github.com/donbing/BlitBot.git

# build the app package
dotnet publish -r linux-arm64 -c Release --nologo --self-contained -p:PublishReadyToRun=true
# run the app package
~/BlitBot/BlitBot/bin/Release/net6.0/linux-arm64/BlitBot
```

# Kiosk setup on ***64bit*** `PiOS lite`

## Install hyperpixel drivers
```sh
# (old, dont do this on PiOS64)
curl https://get.pimoroni.com/hyperpixel4 | bash
# the new way
echo `dtoverlay=vc4-kms-dpi-hyperpixel4sq` | sudo tee -a /boot/config.txt
```
## Install minimal gui packages for loading a browser
```sh
# install minimal gui packages for loading a browser
sudo apt-get install -y --no-install-recommends xserver-xorg x11-xserver-utils xinit openbox chromium-browser

# start x-server on boot
echo '[[ -z $DISPLAY && $XDG_VTNR -eq 1 ]] && startx -- -nocursor' | sudo tee -a ~/.bash_profile

# setup global kiosk url
echo 'export KIOSK_URL=http://localhost:5001/FullChart' | sudo tee -a /etc/xdg/openbox/environment

# Remove exit errors from the config files that could trigger a warning
# sed -i 's/"exited_cleanly":false/"exited_cleanly":true/' ~/.config/chromium/'Local State'
# sed -i 's/"exited_cleanly":false/"exited_cleanly":true/; s/"exit_type":"[^"]\+"/"exit_type":"Normal"/' ~/.config/chromium/Default/Preferences
```

## Autostart chromium in kiosk mode
```sh
echo '
# disable screen power management
xset -dpms            # turn off display power management system
xset s noblank        # turn off screen blanking
xset s off            # turn off screen saver

# run Blitbot
~/BlitBot/BlitBot/bin/Release/net6.0/linux-arm64/BlitBot

# Run Chromium in kiosk mode
chromium-browser  --noerrdialogs --disable-infobars --check-for-update-interval=31536000 --kiosk $KIOSK_URL ' | sudo tee -a  /etc/xdg/openbox/autostart

# set the pi to autologin
sudo raspi-config nonint do_boot_behaviour B2
```

# TODO

 - exchange search input
 - configure when to show the TA widget
 - add VNC server
