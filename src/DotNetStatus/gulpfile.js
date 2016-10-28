/// <binding AfterBuild='build' Clean='clean' />
var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    gulpPath = require('gulp-path'),
    gutil = require('gulp-util');

var assets = new gulpPath.Base({
        src: './assets/',
        dest: './wwwroot/'
    }),
    root = new gulpPath.Base();

var paths = {
    lib: assets.Path('lib'),
    bower: root.Path('bower_components'),
    css: assets.Path('css', 'css'),
    js: assets.Path('js', 'js'),
};

paths.minCss = paths.css.files('/**/*', 'min.css')[0];
paths.minJs = paths.js.files('/**/*', 'min.js')[0];

paths.concatJsDest = gulpPath.filesPaths(paths.js.dest, 'site', 'min.js')[0];
paths.concatCssDest = gulpPath.filesPaths(paths.css.dest, 'site', 'min.css')[0];

gulp.task("clean:js", function (callback) {
    var destination = paths.js.dest[0];
    gutil.log('Cleaning [' + destination + '] ...');
    rimraf(destination, callback);
});

gulp.task("clean:css", function (callback) {
    var destination = paths.css.dest[0];
    gutil.log('Cleaning [' + destination + '] ...');
    rimraf(destination, callback);
});

gulp.task("clean:lib", function (callback) {
    var destination = paths.lib.dest[0];

    gutil.log('Cleaning [' + destination + '] ...');
    rimraf(destination, callback);
});

gulp.task("clean", ["clean:js", "clean:css", "clean:lib"]);

gulp.task("min:js", function () {
    var source = paths.js.src[0];
    var destination = paths.concatJsDest;
    var minifiedFiles = '!' + paths.minJs;

    gutil.log('Concat files from ' + source + ' except [' + minifiedFiles +'] to ' + destination + '...');

    gulp.src([source, minifiedFiles], { base: "." })
        .pipe(concat(destination))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    var source = paths.css.src[0];
    var destination = paths.concatCssDest;
    var minifiedFiles = '!' + paths.minCss;

    gutil.log('Concat files from ' + source + ' except [' + minifiedFiles +'] to ' + destination + '...');

    gulp.src([source, "!" + minifiedFiles])
        .pipe(concat(destination))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("copy", function () {
    var bower = {
        "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
        "jquery": "jquery/dist/jquery*.{js,map}",
        "jquery-ui": "jquery-ui/jquery-ui.js",
        "jquery-ui-smoothness": "jquery-ui-smoothness/jquery-ui.css",
        "chartjs" : "chartjs/Chart*"
    };

    gutil.log('Copying bower_components to wwwroot/lib...');

    for (var module in bower) {

        var bowerSource = paths.bower.files(bower[module])[0]
        var libFolder = gulpPath.filesPaths(paths.lib.dest, module)[0];

        gutil.log('Source [' + bowerSource + '], Destination [' + libFolder + ']');

        gulp.src(bowerSource)
          .pipe(gulp.dest(libFolder));
    };
});

gulp.task("min", ["min:js", "min:css"]);

gulp.task("build", ["copy", "min"]);
