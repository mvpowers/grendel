#!/bin/bash

homedir=/home/ubuntu

cp $homedir/grendel/.env.example $homedir/grendel/.env
cp $homedir/grendel/server/GrendelApi/appsettings.example.json $homedir/grendel/server/GrendelApi/appsettings.json
cp $homedir/grendel/server/GrendelCommon/appsettings.example.json $homedir/grendel/server/GrendelCommon/appsettings.json
