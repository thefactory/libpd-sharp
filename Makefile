all: Pd/libpd.a

libpd/libpd.xcodeproj:
	git submodule update --init --recursive

Pd/libpd.a: libpd/libpd.xcodeproj
	xcodebuild -project "./libpd/libpd.xcodeproj" -sdk iphonesimulator -configuration Release clean build
	xcodebuild -project "./libpd/libpd.xcodeproj" -sdk iphoneos -configuration Release clean build
	lipo -create -output ./libpd/build/libpd.a ./libpd/build/Release-iphoneos/libpd-ios.a ./libpd/build/Release-iphonesimulator/libpd-ios.a
	cp ./libpd/build/libpd.a ./Pd/libpd.a

clean:
	rm -rf libpd
	rm -rf ./Pd/bin
	rm -rf ./Pd/obj
	rm -rf ./Pd/libpd.a

