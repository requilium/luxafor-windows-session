# Luxafor Flag Session Handler for Windows

## Overview

In open space or team spaces, you may wish to indicate to others that you do not wish to be disturbed.  Luxafor is a little controllable LED flag which can be used to do as such.

This tool:

1. Provies a console window to change the LED colors from available to do not distrub, and
1. Automatically turns the flag on and off when you log into windows or (un)lock your computer

## Usage

This project requires Visual Studio to build.

1. Clone the repo
1. Open src/sessionconsole.sln
1. Build the solution
1. Run sessionconsole.exe and keep it open
1. Type "focus", "unfocus", "standup", or q

## Example

![Session Console](docs/console.png?raw=true "Session Console")

## Future Feature Goals

1. Automate the LED colors for availability by tying it with MSFT Team's Presence.  Blocked as Teams does not have an API to get this.
1. Automate the rear LEDs when the computer is locked to match the user's calendar state (normal or on vacation).
1. Automate standup LED pattern based on timer or calendar event trigger.

## License

For personal use only.  Commercial use of this code is prohibited.