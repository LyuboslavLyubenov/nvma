# NVMA

NVMA is a tool that "acts" like the https://github.com/coreybutler/nvm-windows, but without requiring admin rights for installing and using. 
It mimics only essential subset of features that are needed in order to get multiple versions of nodejs. 

## Prerequisites

You must uninstall nodejs, if you had it installed previously.

## How to install

Just download the binary and execute it.

## Usage

Lists all available commands, what they do and how are they used.
```
nvma.exe help
```

Lists all nodejs version that you can use
```
nvma.exe list
```

Changes your version
```
nvma.exe use <version>
nvma.exe use v14.9.0
```

## Issues

If you have any problems, please open a issue.