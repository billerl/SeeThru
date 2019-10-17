# SeeThru

SeeThru is a Windows form built with VB.Net and AdvancedHMI that allows you to 
capture an image based on the size and location of the transparent form.

## Installation

From Git Bash
cd directory/where/you/want/to/clone/SeeThru
$ git clone https://github.com/billerl/SeeThru.git

From Windows Explorer
Navigate to the directory SeeThru is stored
Goto SeeThru -> AdvancedHMI -> bin -> Debug
Right click "AdvancedHMI.exe" and create a shortcut or pin to taskbar

## Usage

Open shortcut created above
To setup the PLC communication
select Edit -> Preferences -> Use PLC

 <img src="http://lukebiller.com/Images/UsePLC.png">

 The SetIP form will show. 
 Enter the appropriate information.
 Currently only Allen Bradley Slc / Micrologix or Compact / ControlLogix is supported via Ethernet

 <img src="http://lukebiller.com/Images/SetIP.png">

 On the Main page click start. 
 Once the Plc Address assinged as the trigger examines True the image will be captured

 On first run you'll be prompted to create a folder named "SeeThru" in you MyPictures folder where the captures will be stored.
 
 <img src="http://lukebiller.com/Images/CreateFolder.png">


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)