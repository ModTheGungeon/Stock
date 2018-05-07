#!/usr/bin/sh
set -e

killall EtG.x86_64 || true
xbuild
path="$PWD"
pushd ~/".local/share/Steam/steamapps/common/Enter the Gungeon/EtG_Data/Managed"
cp ORIGINAL_UnityEngine.dll UnityEngine.dll
cp "$path/Stock/bin/Debug/MonoMod.exe" .
cp "$path/Stock/bin/Debug/Mono.Cecil"*".dll" .
cp "$path/Stock/bin/Debug/UnityEngine.Stock.mm.dll" .
cp "$path/Stock/bin/Debug/Logger.dll" .
cp "$path/Stock/bin/Debug/Eluant.dll" .
mono MonoMod.exe UnityEngine.dll
mv MONOMODDED_UnityEngine.dll UnityEngine.dll
popd
steam steam://run/311690
