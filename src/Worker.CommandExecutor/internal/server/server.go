/*
MIT License

Copyright The pipeline-manager Authors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

package server

import (
	"io"
	"net"

	"golang.org/x/net/context"
	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"

	"github.com/RyazanovAlexander/pipeline-manager/command-executor/v1/config"
	"github.com/RyazanovAlexander/pipeline-manager/command-executor/v1/internal/executor"
)

type server struct {
	out io.Writer

	UnimplementedExecServiceServer
}

func (s *server) ExecuteCommand(ctx context.Context, in *ExecCommand) (*ExecResult, error) {
	result := true
	errorMessage := ""

	err := executor.ExecCommand(in.Command, s.out)
	if err != nil {
		result = false
		errorMessage = err.Error()
	}

	return &ExecResult{Result: result, ErrorMessage: errorMessage}, nil
}

func Run(out io.Writer) error {
	listner, err := net.Listen("tcp", config.Config.ServerGrpcPort)
	if err != nil {
		return err
	}

	grpcServer := grpc.NewServer()
	executorServer := server{
		out: out,
	}

	RegisterExecServiceServer(grpcServer, &executorServer)
	reflection.Register(grpcServer)

	if err := grpcServer.Serve(listner); err != nil {
		return err
	}

	return nil
}
