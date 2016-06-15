/// <binding BeforeBuild='build' Clean='clean' />
var gulp = require("gulp"),
    merge = require("merge-stream"),
    rimraf = require("rimraf");

// Paths
var webroot = "./wwwroot/";
var paths = {
    webroot: webroot,
    libDest: webroot + "lib/",
    cssDest: webroot + "css/",
    node_modules: "./node_modules/"
};

gulp.task("clean:libs", function (cb) {
    rimraf(paths.libDest, cb);
});

//gulp.task("clean:css", function (cb) {
//    rimraf(paths.cssDest, cb);
//});

gulp.task("clean", ["clean:libs"]);

gulp.task("copy:libs", ["clean"], function () {
    var angular2 = gulp.src(paths.node_modules + "angular2/bundles/**/*.js")
        .pipe(gulp.dest(paths.libDest + "angular2"));

    var es6_shim = gulp.src([
            paths.node_modules + "es6-shim/*.js",
            "!**/Gruntfile.js"])
        .pipe(gulp.dest(paths.libDest + "es6-shim"));

    var systemjs = gulp.src(paths.node_modules + "systemjs/dist/*.js")
        .pipe(gulp.dest(paths.libDest + "systemjs"));

    var rxjs = gulp.src(paths.node_modules + "rxjs/bundles/**/*.js")
        .pipe(gulp.dest(paths.libDest + "rxjs"));

    var corejs = gulp.src(paths.node_modules + "core-js/client/**/*.js")
        .pipe(gulp.dest(paths.libDest + "core-js"));

    var zonejs = gulp.src(paths.node_modules + "zone.js/dist/**/*.js")
        .pipe(gulp.dest(paths.libDest + "zone.js"));

    var reflect = gulp.src(paths.node_modules + "reflect-metadata/Reflect.js")
        .pipe(gulp.dest(paths.libDest + "reflect-metadata"));

    return merge(angular2, es6_shim, systemjs, rxjs, corejs, zonejs, reflect);
});

gulp.task("build", ["copy:libs"]);




///// <binding Clean='clean' />
//"use strict";

//var gulp = require("gulp"),
//    rimraf = require("rimraf"),
//    concat = require("gulp-concat"),
//    cssmin = require("gulp-cssmin"),
//    uglify = require("gulp-uglify");

//var webroot = "./wwwroot/";

//var paths = {
//    js: webroot + "js/**/*.js",
//    minJs: webroot + "js/**/*.min.js",
//    css: webroot + "css/**/*.css",
//    minCss: webroot + "css/**/*.min.css",
//    concatJsDest: webroot + "js/site.min.js",
//    concatCssDest: webroot + "css/site.min.css"
//};

//gulp.task("clean:js", function (cb) {
//    rimraf(paths.concatJsDest, cb);
//});

//gulp.task("clean:css", function (cb) {
//    rimraf(paths.concatCssDest, cb);
//});

//gulp.task("clean", ["clean:js", "clean:css"]);

//gulp.task("min:js", function () {
//    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
//        .pipe(concat(paths.concatJsDest))
//        .pipe(uglify())
//        .pipe(gulp.dest("."));
//});

//gulp.task("min:css", function () {
//    return gulp.src([paths.css, "!" + paths.minCss])
//        .pipe(concat(paths.concatCssDest))
//        .pipe(cssmin())
//        .pipe(gulp.dest("."));
//});

//gulp.task("min", ["min:js", "min:css"]);

