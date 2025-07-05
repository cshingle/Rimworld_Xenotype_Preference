#!/bin/sh

FrameworkPathOverride=$(dirname $(which mono))/../lib/mono/4.7.2-api/ dotnet build *.csproj /property:Configuration=Release

rm -rf ../1.6/
mkdir ../1.6
mkdir ../1.6/Assemblies/
cp bin/Release/Zig158.XenotypePreference.dll ../1.6/Assemblies/

rm -rf $RIMWORLD_MOD_DIR/XenotypePreference
mkdir $RIMWORLD_MOD_DIR/XenotypePreference
mkdir $RIMWORLD_MOD_DIR/XenotypePreference/About
mkdir $RIMWORLD_MOD_DIR/XenotypePreference/1.5
mkdir $RIMWORLD_MOD_DIR/XenotypePreference/1.6

cp $(realpath ../)/LICENSE $(realpath $RIMWORLD_MOD_DIR/XenotypePreference)/LICENSE
cp -r $(realpath ../)/About/* $(realpath $RIMWORLD_MOD_DIR/XenotypePreference)/About/
cp -r $(realpath ../)/1.5/* $(realpath $RIMWORLD_MOD_DIR/XenotypePreference)/1.5/
cp -r $(realpath ../)/1.6/* $(realpath $RIMWORLD_MOD_DIR/XenotypePreference)/1.6/