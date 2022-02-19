# BlitBot
> Blazor Server app for showing market charts

- uses trading view widget
- config editor web-page allows live updates to the chart view on other devices.

> shell cmds for kiosk setup (based on buster lite image, bullseye fails)
'''sh

# install hyperpixel drivers
curl https://get.pimoroni.com/hyperpixel4 | bash

# install minimal gui packages
sudo apt-get install -y --no-install-recommends xserver-xorg x11-xserver-utils xinit openbox
sudo apt-get install -y --no-install-recommends chromium-browser

# setup gui autostart
echo '
# disable screen power management
xset -dpms            # turn off display power management system
xset s noblank        # turn off screen blanking
xset s off            # turn off screen saver

# Remove exit errors from the config files that could trigger a warning
sed -i 's/"exited_cleanly":false/"exited_cleanly":true/' ~/.config/chromium/'Local State'
sed -i 's/"exited_cleanly":false/"exited_cleanly":true/; s/"exit_type":"[^"]\+"/"exit_type":"Normal"/' ~/.config/chromium/Default/Preferences

# Run Chromium in kiosk mode
chromium-browser  --noerrdialogs --disable-infobars --check-for-update-interval=31536000 --kiosk $KIOSK_URL 
' | sudo tee -a  /etc/xdg/openbox/autostart

# setup global kiosk url
echo '[[ -z $DISPLAY && $XDG_VTNR -eq 1 ]] && startx -- -nocursor' | sudo tee -a ~/.bash_profile

# start x-server on boot
echo '$KIOSK_URL=http://localhost/FullChart' | sudo tee -a /etc/xdg/openbox/environment

# set the pi to autologin
raspi-config nonint do_boot_behaviour B2



# setup .net
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel Current
echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
source ~/.bashrc
# check it worked
dotnet --version

git clone https://github.com/donbing/BlitBot.git
cd Blitbot/BlitBot
dotnet run

'''