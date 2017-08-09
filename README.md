# coscup2017_grpc_csharp
COSCUP 2017 C# gRPC client code

### To build

1. Install .net Core SDK 1.1: https://www.microsoft.com/net/core

2. If you don't use [git submodule which is supported on github](https://github.com/blog/2104-working-with-submodules), Remember to fork [proto git repo](https://github.com/windperson/coscup2017_grpc_proto.git ) under **proto** subdirectory.

3. Generate C# code as the instructions reside in each proto git repo subfolder.  

4. Invoke [`dotnet build`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build ) command.

### To run 

The easiet way is using [Visual Studio Code](https://code.visualstudio.com/)'s .net core [debugging capability](https://code.visualstudio.com/docs/editor/debugging),

or build as a executable file then run with two command line arguments: **remote_url** and **word_file_generate_path**