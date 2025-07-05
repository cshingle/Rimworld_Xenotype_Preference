#!/bin/sh

FrameworkPathOverride=$(dirname $(which mono))/../lib/mono/4.7.2-api/ dotnet build *.csproj /property:Configuration=Debug

rm -rf ../1.6/
mkdir ../1.6
mkdir ../1.6/Assemblies/

ln -s $(realpath bin/Debug)/Zig158.XenotypePreference.dll $(realpath ../1.6/Assemblies/)/Zig158.XenotypePreference.dll
ln -s $(realpath bin/Debug)/Zig158.XenotypePreference.pdb $(realpath ../1.6/Assemblies/)/Zig158.XenotypePreference.pdb

rm -rf $RIMWORLD_MOD_DIR/XenotypePreference
mkdir $RIMWORLD_MOD_DIR/XenotypePreference
ln -s $(realpath ../)/LICENSE $(realpath $RIMWORLD_MOD_DIR/XenotypePreference)/LICENSE
ln -s $(realpath ../)/About $(realpath $RIMWORLD_MOD_DIR/XenotypePreference)/About
ln -s $(realpath ../)/1.5 $(realpath $RIMWORLD_MOD_DIR/XenotypePreference)/1.5
ln -s $(realpath ../)/1.6 $(realpath $RIMWORLD_MOD_DIR/XenotypePreference)/1.6