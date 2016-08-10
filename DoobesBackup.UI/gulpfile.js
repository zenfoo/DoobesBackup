/// <binding BeforeBuild='build' Clean='clean' />
var gulp = require("gulp"),
    merge = require("merge-stream"),
    rimraf = require("rimraf"),
    ts = require("gulp-typescript"),
    runSequence = require("run-sequence");


var buildErrors = false;


// Paths
var webroot = "./";
var paths = {
    webroot: webroot,
    libDest: webroot + "lib/",
    cssDest: webroot + "css/",
    node_modules: "./node_modules/",
    tsSource: webroot + "app/**/*.ts",
    tsDefSource: "./typings/**/*.d.ts",
    tsOutput: webroot + "app",
    tsDefOutput: "./typings/app"
};

var tsProject = ts.createProject("./tsconfig.json");

gulp.task("ts:compile", function () {
    
    var tsResult = gulp
        .src([
            paths.tsSource,
            paths.tsDefSource
        ])
        .pipe(
            ts(
                tsProject,
                undefined,
                ts.reporter.fullReporter(true)))
        .on("error", onError)
        .on("end", handleErrors);

    return merge(
        tsResult.dts.pipe(gulp.dest(paths.tsDefOutput)),
        tsResult.js.pipe(gulp.dest(paths.tsOutput)));
});


gulp.task("clean:libs", function (cb) {
    rimraf(paths.libDest, cb);
});

//gulp.task("clean:css", function (cb) {
//    rimraf(paths.cssDest, cb);
//});

gulp.task("clean", ["clean:libs"]);

gulp.task("copy:libs", function () {
    var angular2 = gulp.src(paths.node_modules + '@angular/**/*.js')
        .pipe(gulp.dest(paths.libDest + '@angular'));

    var angular2InMemoryWebApi = gulp.src(paths.node_modules + "angular2-in-memory-web-api/**/*.js")
        .pipe(gulp.dest(paths.libDest + "angular2-in-memory-web-api"));

    var es6_shim = gulp.src([
            paths.node_modules + "es6-shim/*.js",
            "!**/Gruntfile.js"])
        .pipe(gulp.dest(paths.libDest + "es6-shim"));

    var systemjs = gulp.src(paths.node_modules + "systemjs/dist/*.js")
        .pipe(gulp.dest(paths.libDest + "systemjs"));

    var rxjs = gulp.src(paths.node_modules + "rxjs/**/*.js")
        .pipe(gulp.dest(paths.libDest + "rxjs"));

    var corejs = gulp.src(paths.node_modules + "core-js/client/**/*.js")
        .pipe(gulp.dest(paths.libDest + "core-js"));

    var zonejs = gulp.src(paths.node_modules + "zone.js/dist/**/*.js")
        .pipe(gulp.dest(paths.libDest + "zone-js"));

    var reflect = gulp.src(paths.node_modules + "reflect-metadata/Reflect.js")
        .pipe(gulp.dest(paths.libDest + "reflect-metadata"));

    var bootstrap = gulp.src(paths.node_modules + "bootstrap/dist/**/*")
        .pipe(gulp.dest(paths.libDest + "bootstrap"));

    var gentelella = gulp.src(paths.node_modules + "gentelella/build/**/*")
        .pipe(gulp.dest(paths.libDest + "gentelella"));

    var jquery = gulp.src(paths.node_modules + "jquery/dist/**/*.js")
        .pipe(gulp.dest(paths.libDest + "jquery"));

    var fontawesome = gulp.src(paths.node_modules + "font-awesome/**/*")
        .pipe(gulp.dest(paths.libDest + "font-awesome"));

    return merge(
        angular2,
        angular2InMemoryWebApi,
        es6_shim,
        systemjs,
        rxjs,
        corejs,
        zonejs,
        reflect,
        bootstrap,
        gentelella,
        jquery,
        fontawesome);
});


gulp.task('build', function (done) {
    runSequence("clean", 'ts:compile', 'copy:libs', function () {
        console.log('Build sequence completed!');
        done();
    });
});

function handleErrors() {
    if (buildErrors) {
        process.exit(1);
    }
}

function onError(error)
{
    buildErrors = true;
    console && console.log(error.message);
}

function onWarn(error) {
    console && console.log(error.message);
}

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

