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

package cmd

import (
	"fmt"
	"io"

	"github.com/spf13/cobra"

	"github.com/RyazanovAlexander/pipeline-manager/command-executor/v1/internal/server"
)

var globalUsage = `Agent for executing pipeline steps.

Common actions for command-executor:

- command-executor: starts grpc service and waits for execution commands
`

// NewRootCmd creates new root cmd.
func NewRootCmd(out io.Writer, args []string) *cobra.Command {
	cmd := &cobra.Command{
		Use:   "command-executor",
		Short: "Starts grpc service and waits for execution commands",
		Long:  globalUsage,
		Run:   func(cmd *cobra.Command, args []string) { runRootCmd(out, args) },
	}

	flags := cmd.PersistentFlags()
	flags.Parse(args)

	// Add subcommands
	cmd.AddCommand(
		newVersionCmd(out),
	)

	return cmd
}

func runRootCmd(out io.Writer, args []string) {
	err := server.Run(out)
	if err != nil {
		fmt.Fprintln(out, err)
	}
}
