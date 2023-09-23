mkdir %appdata%\LDevs\DiSHLoader
mkdir %appdata%\LDevs\DiSHLoader\versions
copy * %appdata%\LDevs\DiSHLoader\
mklink "%appdata%\Microsoft\Windows\Start Menu\Programs\Startup\DiSHLoader.exe" %appdata%\LDevs\DiSHLoader\DiSHLoader.exe
echo "Setup done! You just need an env file and a version"