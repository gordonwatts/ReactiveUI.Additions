#
# Build the nuspec, and push to myget
#
param([parameter(Mandatory = $true)][string] $ApiKey)

$version="1.0.0-alpha02"

set-alias nuget .\.nuget\nuget.exe

ls *.nupkg | % { rm $_ }

nuget pack -symbols -Properties version=$version ReactiveUI.Additions.nuspec
nuget push ReactiveUI.Additions.$version.nupkg -source https://www.myget.org/F/mvvm/ -ApiKey $ApiKey
nuget push ReactiveUI.Additions.$version.symbols.nupkg -source https://nuget.symbolsource.org/MyGet/mvvm -ApiKey $ApiKey
