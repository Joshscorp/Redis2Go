$keyfile = "C:\NugetKey\nuget-access-key.txt"
$scriptpath = split-path -parent $MyInvocation.MyCommand.Path
$nugetpath = resolve-path "$scriptpath/src/.nuget/nuget.exe"
$packagespath = resolve-path "$scriptpath/nugetpackage"
 
if(-not (test-path $keyfile)) {
  throw "Could not find the NuGet access key at $keyfile. If you're not Josh, you shouldn't be running this script!"
}
else {
  pushd $packagespath
 
  # get our secret key. This is not in the repository.
  $key = get-content $keyfile
 
  # Find all the packages and display them for confirmation
  $packages = dir "*.nupkg"
  write-host "Packages to upload:"
  $packages | % { write-host $_.Name }
 
  # upload
  $packages | % { 
      $package = $_.Name
      write-host "Uploading $package"
      & $nugetpath push $package $key
      write-host ""
  }
  popd
}