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
	rmdir('build')

	with FileList():
		.Include("src/RealEx/bin/${configuration}/RealEx.*")
		.Include("License.txt")
		.Include("readme.md")
		.Flatten(true)
		.ForEach def(file):
			file.CopyToDirectory("build/${configuration}")

desc "Creates zip package"
target package:
	zip("build/${configuration}", "build/RealEx.zip")