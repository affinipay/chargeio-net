require 'albacore'

VERSION = "1.0.0"

task :default => [:build, :merge, :output, :package]

assemblyinfo :assemblyinfo do |asm|
	asm.version = VERSION
	asm.company_name = "ChargeIO LLC"
	asm.product_name = "chargeIO.net"
	asm.title = "chargeIO.net"
	asm.description = "A .net client api for http://chargeio.com"
	asm.copyright = "Copyright (C) ChargeIO LLC 2013"
	asm.output_file = "src/SharedAssemblyInfo.cs"
end

task :build => [:build40, :build35]
task :merge => [:merge40, :merge35]


desc "Build40"
msbuild :build40 => :assemblyinfo do |msb|
	msb.properties :configuration => :Release
	msb.targets :Clean, :Build
	msb.solution = "src/chargeIO.sln"
	puts 'Net40 Solution built'
end

desc "Build35"
msbuild :build35 => :assemblyinfo do |msb|
	msb.properties :configuration => :Release
	msb.targets :Clean, :Build
	msb.solution = "src/chargeIO.Net35.sln"
	puts 'Net35 Solution built'
end

desc "Merge40"
exec :merge40 do |cmd|
	cmd.command = 'tools\ilmerge\ilmerge.exe'
	cmd.parameters ='/out:src\chargeIO\bin\release\Net40\chargeIO.net.dll /targetplatform:v4,"C:\Windows\Microsoft.NET\Framework\v4.0.30319" src\chargeIO\bin\release\Net40\chargeIO.dll src\chargeIO\bin\release\Net40\Newtonsoft.Json.dll /closed /internalize'
	puts 'Net40 Merging complete'
end

desc "Merge35"
exec :merge35 do |cmd|
	cmd.command = 'tools\ilmerge\ilmerge.exe'
	cmd.parameters ='/out:src\chargeIO\bin\release\Net35\chargeIO.net.dll /lib:"C:\Windows\Microsoft.NET\Framework\v3.5" src\chargeIO\bin\release\Net35\chargeIO.dll src\chargeIO\bin\release\Net35\Newtonsoft.Json.dll /closed /internalize'
	puts 'Net35 Merging complete'
end

desc "Output"
output :output do |out|
	out.from '.'
	out.to 'working'
	out.file 'src\chargeIO\bin\release\Net40\chargeIO.net.dll', :as=>'Net40\chargeIO.net.dll'
	out.file 'src\chargeIO\bin\release\Net35\chargeIO.net.dll', :as=>'Net35\chargeIO.net.dll'
	puts 'Output folder created'
end

desc "Package"
zip :package do |zip|
	zip.directories_to_zip "working"
	zip.output_file = "chargeIO.net #{VERSION}.zip"
	zip.output_path = File.join(File.dirname(__FILE__), 'build')
	puts 'Packing complete'
end
