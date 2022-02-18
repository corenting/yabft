RUNTIME_IDENTIFIER?=linux-x64
BUILD_FOLDER=src/bin/Release/net6.0/$(RUNTIME_IDENTIFIER)/publish

.PHONY: release
release:
	dotnet publish src -r $(RUNTIME_IDENTIFIER) -c Release --self-contained -p:PublishTrimmed=true -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:IncludeNativeLibrariesInSingleFile=true
	rm src/bin/Release/net6.0/$(RUNTIME_IDENTIFIER)/publish/yabft.pdb
	mkdir -p build
	zip -r -j build/yabft_$(RUNTIME_IDENTIFIER).zip $(BUILD_FOLDER)/yabft*

.PHONY: release-all
release-all:
	RUNTIME_IDENTIFIER=linux-x64 make release
	RUNTIME_IDENTIFIER=linux-musl-x64 make release
	RUNTIME_IDENTIFIER=linux-arm make release
	RUNTIME_IDENTIFIER=linux-arm64 make release
	RUNTIME_IDENTIFIER=win-x64 make release
	RUNTIME_IDENTIFIER=win-arm64 make release

.PHONY: clean
clean:
	dotnet clean
	rm -rf build

.PHONE: run
run:
	dotnet
