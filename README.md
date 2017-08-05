# coscup2017_grpc_csharp
COSCUP 2017 C# gRPC client code

### To build

1. Install .net Core SDK 1.1: https://www.microsoft.com/net/core

2. Fork [proto git repo](https://github.com/windperson/coscup2017_grpc_proto.git ) under current proto subdirectory.

3. Generate C# code as the instructions reside in each proto git repo subfolder.  

4. Invoke [`dotnet build`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build ) command.

### To run 

Use [Visual Studio Code](https://code.visualstudio.com/)'s .net core [debugging capability](https://code.visualstudio.com/docs/editor/debugging)