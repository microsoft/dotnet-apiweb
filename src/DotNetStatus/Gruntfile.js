// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

/// <binding BeforeBuild='bower, concat' ProjectOpened='bower:install' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        bower: {
            install: {
                options: {
                    targetDir: "wwwroot/lib",
                    layout: "byComponent",
                    cleanTargetDir: true
                }
            }
        },

        concat: {
            js: {
                src: "Assets/js/*.js",
                dest: "wwwroot/js/site.js"
            },

            css: {
                src: "Assets/css/*.css",
                dest: "wwwroot/css/site.css"
            }
        },
    });

    grunt.registerTask("default", ["bower:install concat"]);

    grunt.loadNpmTasks("grunt-bower-task");
    grunt.loadNpmTasks("grunt-contrib-concat");
};
