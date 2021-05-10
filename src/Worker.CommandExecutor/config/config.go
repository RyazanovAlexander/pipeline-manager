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

package config

import (
	"github.com/spf13/viper"
)

// Config is global object that holds all application level variables.
var Config appConfig

type appConfig struct {
	Debug bool `mapstructure:"DEBUG"`

	ExecutorName   string `mapstructure:"NAME"`
	ServerGrpcPort string `mapstructure:"SERVER_GRPC_PORT"`
}

// Load loads config from environment variables
func Load() error {
	viper.AutomaticEnv()
	viper.SetEnvPrefix("COMMAND_EXECUTOR")

	viper.BindEnv("DEBUG")
	viper.BindEnv("NAME")
	viper.BindEnv("SERVER_GRPC_PORT")

	if err := viper.Unmarshal(&Config); err != nil {
		return err
	}

	return nil
}
