/*global module */
module.exports = function (grunt) {
    'use strict';
    require('load-grunt-tasks')(grunt);
    require('time-grunt')(grunt);
    grunt.loadNpmTasks('grunt-karma');

    grunt.initConfig({
        // read in the project settings from the package.json file into the pkg property
        pkg: grunt.file.readJSON('package.json'),
        // define configuration for each of the tasks we have
        // this is a sample jshint task config
        jshint: {
            // define the files to lint
            files: ['gruntfile.js', 'src/*.js', '/*.js'],
            // configure JSHint (documented at http://www.jshint.com/docs/)
            options: {
                // more options here if you want to override JSHint defaults
                globals: {
                    jQuery: true,
                    console: true,
                    module: true
                }
            }
        },
        karma: {
            unit: {
                configFile: 'karma.conf.js'
            }
        },
        'swagger-js-codegen': {
            queries: {
                options: {
                    apis: [
                        {
                            swagger: 'http://localhost:67833/swagger/docs/v1',
                            // This is the model and file name
                            moduleName: 'Model',
                            className: 'SampleApi',
                            type: 'angular'
                        }
                    ],
                    dest: 'lib'
                },
                dist: {}
            }
        }

    });
    // Default task.
    grunt.registerTask('unittest', ['karma:unit:run']);
    grunt.registerTask('default', ['karma:unit']);
    // this would be run by typing "grunt test" on the command line
    // the array should contains the names of the tasks to run
    // define the default task that can be run just by typing "grunt" on the command line
    // the array should contains the names of the tasks to run
    grunt.registerTask('default', ['swagger-js-codegen']);

};

