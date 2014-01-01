#
# Build the nuspec, and push to myget
#
param([parameter(Mandatory = $true)][string] $ApiKey)

set-alias nuget .\.nuget\nuget.exe

ls *.nupkg | % { rm $_ }

nuget pack -symbols ReactiveUI.Additions.nuspec
nuget push ReactiveUI.Additions.1.0.0-alpha01.nupkg -source https://www.myget.org/F/mvvm/ -ApiKey $ApiKey
nuget push ReactiveUI.Additions.1.0.0-alpha01.symbols.nupkg -source https://nuget.symbolsource.org/MyGet/mvvm -ApiKey $ApiKey
