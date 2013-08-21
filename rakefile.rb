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

desc "Build"
msbuild :build => :assemblyinfo do |msb|
	msb.properties :configuration => :Release
	msb.targets :Clean, :Build
	msb.solution = "src/chargeIO.sln"
	puts 'Solution built'
end

desc "Merge"
exec :merge do |cmd|
	cmd.command = 'tools\ilmerge\ilmerge.exe'
	cmd.parameters ='/out:src\chargeIO\bin\release\chargeIO.net.dll /targetplatform:v4,"C:\Windows\Microsoft.NET\Framework\v4.0.30319" src\chargeIO\bin\release\chargeIO.dll src\chargeIO\bin\release\Newtonsoft.Json.dll /closed /internalize'
	puts 'Merging complete'
end

desc "Output"
output :output do |out|
	out.from '.'
	out.to 'working'
	out.file 'src\chargeIO\bin\release\chargeIO.net.dll', :as=>'chargeIO.net.dll'
	puts 'Output folder created'
end

desc "Package"
zip :package do |zip|
	zip.directories_to_zip "working"
	zip.output_file = "chargeIO.net #{VERSION}.zip"
	zip.output_path = File.join(File.dirname(__FILE__), 'build')
	puts 'Packing complete'
end
