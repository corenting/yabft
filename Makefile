AVAILABLE_RUNTIMES= linux-x64 linux-musl-x64 linux-arm64 win-x64 win-arm64 osx-x64
RUNTIME_IDENTIFIER?=linux-x64

BUILD_FOLDER=src/bin/Release/net8.0/$(RUNTIME_IDENTIFIER)/publish


.PHONY: release
release:
	dotnet publish src -r $(RUNTIME_IDENTIFIER) -c Release --self-contained -p:PublishTrimmed=true -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:IncludeNativeLibrariesInSingleFile=true
	rm src/bin/Release/net8.0/$(RUNTIME_IDENTIFIER)/publish/yabft.pdb
	mkdir -p build
	zip -r -j build/yabft_$(RUNTIME_IDENTIFIER).zip $(BUILD_FOLDER)/yabft*

.PHONY: release-all
release-all:
	$(foreach var,$(AVAILABLE_RUNTIMES), RUNTIME_IDENTIFIER=$(var) make release;)

.PHONY: init
init:
	 dotnet restore

.PHONY: test
test:
	dotnet test --no-restore --collect:"XPlat Code Coverage"

.PHONY: build
build:
	dotnet build --no-restore

.PHONY: clean
clean:
	dotnet clean
	rm -rf build
	rm -rf tests/TestResults
