solution_file = "RealEx.sln"
configuration = "release"
test_assemblies = "src/RealEx.Tests/bin/${configuration}/RealEx.Tests.dll"

target default, (compile, test, deploy, package):
	pass

desc "Compiles solution"	
target compile:
	msbuild(file: solution_file, configuration: configuration, version: "4.0")

desc "Executes unit tests"
target test:
	mspec(assembly: test_assemblies)

desc "Copies binaries to the build directory"
target deploy:
	rm('build')

	with FileList():
		.Include("src/**/bin/${configuration}/RealEx.*")
		.Exclude("src/**/bin/${configuration}/RealEx.Tests.*")
		.Include("License.txt")
		.Include("readme.md")
		.Flatten(true)
		.ForEach def(file):
			file.CopyToDirectory("build/${configuration}")

desc "Creates zip and nuget packages"
target package:
	zip("build/${configuration}", "build/RealEx.zip")

	nuget_pack(toolPath: ".nuget/nuget.exe", nuspecFile: "realex.nuspec", outputDirectory: "build/nuget")

desc "Publishes nuget package"
target publish:
	apiKey = env("apiKey")

	with FileList():
		.Include("build/nuget/*.nupkg")
		.Flatten(true)
		.ForEach def(file):
			exec("echo publishing ${file.FullName}")
			nuget_push(toolPath: ".nuget/nuget.exe", apiKey: apiKey, packagePath: file.FullName)