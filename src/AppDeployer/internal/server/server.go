/*
MIT License

Copyright The deployer Authors.

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
	"net/http"

	"github.com/gin-gonic/gin"

	"github.com/RyazanovAlexander/pipeline-manager/deployer/v1/internal/deployer"
)

type Manifest struct {
	RepoName  string `json:"repoName" binding:"required"`
	RepoUrl   string `json:"repoUrl" binding:"required"`
	ChartName string `json:"chartName" binding:"required"`
}

func Run(out io.Writer) error {
	router := gin.Default()

	router.POST("/deploy/manifest", func(c *gin.Context) {
		var manifest Manifest
		if err := c.ShouldBindJSON(&manifest); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		if manifest.RepoName == "" || manifest.RepoUrl == "" || manifest.ChartName == "" {
			c.JSON(http.StatusBadRequest, "Incorrect parameters passed")
			return
		}

		if err := deployer.Deploy(manifest.RepoName, manifest.RepoUrl, manifest.ChartName, out); err != nil {
			c.JSON(http.StatusInternalServerError, err.Error)
			return
		}

		c.JSON(http.StatusOK, "Resource deployed successfully")
	})

	router.Run(":80")
	return nil
}
