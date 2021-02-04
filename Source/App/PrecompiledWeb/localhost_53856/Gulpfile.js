/*
 * 
 npm install --save gulp gulp-plumber gulp-changed gulp-minify-html gulp-autoprefixer gulp-minify-css gulp-uglify gulp-imagemin gulp-rename gulp-concat gulp-strip-debug gulp-notify gulp-livereload del gulp-inject gulp-jshint@2.x gulp-replace
 *
 */

/*
 * 
 npm uninstall gulp gulp-plumber gulp-changed gulp-minify-html gulp-autoprefixer gulp-minify-css gulp-uglify gulp-imagemin gulp-rename gulp-concat gulp-strip-debug gulp-notify gulp-livereload del gulp-inject gulp-jshint@2.x gulp-replace
 * 
 */

var oldVersionNo = "v1.0.3";
var newVersionNo = "v1.0.5";

var localServerBaseUrl = "http://localhost/payslip/"; 
//var productionServerBaseUrl = "https://payslip.upriver.io/server/";
var productionServerBaseUrl = "http://localhost/payslipserver/";
//var productionServerBaseUrl = "http://192.168.1.12/payslip/server/";  
      
var templateUrlDevelopmentDirectory = "app/views/";
var templateUrlProductionDirectory = "dist/" + newVersionNo + "/views/";
   
var footerOldVersion = "Version: 1.0.3";
var footerNewVersion = "Version: 1.0.4";       

//var indexTemplateUrlOldDirectory = "dist/views/" + oldVersionNo + "/";
//var indexTemplateUrlNewDirectory = "dist/views/" + newVersionNo + "/";


var gulp = require("gulp"),
    
    changed = require("gulp-changed"),
    imagemin = require("gulp-imagemin"),
    notify = require("gulp-notify"),

	minifyHTML = require("gulp-minify-html"),

	stripDebug = require("gulp-strip-debug"),
	jshint = require("gulp-jshint"),
    plumber = require("gulp-plumber"),
    concat = require("gulp-concat"),
    uglify = require("gulp-uglify"),
        
    autoprefixer = require("gulp-autoprefixer"),
    minifyCSS = require("gulp-minify-css"),
    
	livereload = require("gulp-livereload"),
    del = require("del"),    
    
    inject = require("gulp-inject");

    replace = require("gulp-replace");




// minify new images
gulp.task("images", function () {
    var imgSrc = "./app/images/**/*",
        imgDst = "./dist/" + newVersionNo + "/images";

    gulp.src(imgSrc)
        .pipe(changed(imgDst))
        .pipe(imagemin())
        .pipe(gulp.dest(imgDst))
        //.pipe(notify({ message: 'images task complete' }))
    ;
});

// minify new or changed HTML pages
gulp.task("htmls", function () {
    var htmlSrc = ["./app/views/**/*.html", "!./app/index.html", "!./app/dev.html"],
        htmlDst = "./dist/"  + newVersionNo + "/views/";

    gulp.src(htmlSrc)
        .pipe(changed(htmlDst))
        //.pipe(minifyHTML())
        .pipe(gulp.dest(htmlDst))
        //.pipe(notify({ message: 'htmls task complete' }))
    ;
});



// JS concat & uglify 
gulp.task("scripts", function () {

    gulp.src(["./app/scripts/app.config.js","./app/scripts/**/*.config.js"])        
        .pipe(jshint())
        .pipe(jshint.reporter("default"))
        .pipe(plumber())
        .pipe(concat("config-scripts.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"))
        .pipe(notify({ message: 'config scripts task complete' }))
    ;

    gulp.src(["./app/scripts/app.directive.js", "./app/scripts/**/*.directive.js"])
       .pipe(jshint())
       .pipe(jshint.reporter("default"))
       .pipe(plumber())
       .pipe(concat("directive-scripts.min.js"))
       .pipe(uglify())
       .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"))
       .pipe(notify({ message: 'directive scripts task complete' }))
    ;

    gulp.src(["./app/scripts/app.service.js", "./app/scripts/**/*.service.js"])
       .pipe(jshint())
       .pipe(jshint.reporter("default"))
       .pipe(plumber())
       .pipe(concat("service-scripts.min.js"))
       .pipe(uglify())
       .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"))
       .pipe(notify({ message: 'service scripts task complete' }))
    ;

    gulp.src(["./app/scripts/app.controller.js", "./app/scripts/**/*.controller.js"])
       .pipe(jshint())
       .pipe(jshint.reporter("default"))
       .pipe(plumber())
       .pipe(concat("controller-scripts.min.js"))
       .pipe(uglify())
       .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"))
       .pipe(notify({ message: 'controller scripts task complete' }))
    ;
    
});


// CSS concat, auto-prefix and minify
gulp.task("styles", function () {
    gulp.src(["./app/styles/**/*.css"])
        .pipe(concat("style.min.css"))
        .pipe(autoprefixer("last 2 versions"))
        .pipe(minifyCSS())
        .pipe(gulp.dest("./dist/" + newVersionNo + "/styles/"))
        //.pipe(notify({ message: 'styles task complete' }))
    ;
    
});


//replace tasks
gulp.task("replace-templateurl", function () {    
    gulp.src(["./dist/" + newVersionNo + "/scripts/**/*.min.js"])
      .pipe(replace(templateUrlDevelopmentDirectory, templateUrlProductionDirectory))
      .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"));        
});

gulp.task("replace-serverurl", function() {
    gulp.src(["./dist/" + newVersionNo + "/scripts/**/*.min.js"])
      .pipe(replace(localServerBaseUrl, productionServerBaseUrl))
      .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"));
});

gulp.task("replace-index", function() {
    gulp.src(["./index.html"])
      .pipe(replace(oldVersionNo, newVersionNo))
      .pipe(gulp.dest("./"));

    //gulp.src(["./index.html"])
    //  .pipe(replace(indexTemplateUrlOldDirectory, indexTemplateUrlNewDirectory))
    //  .pipe(gulp.dest("./"));
});

gulp.task("replace-footer-version", function () {
    gulp.src(["./dist/" + newVersionNo + "/scripts/**/*.min.js"])
      .pipe(replace(footerOldVersion, footerNewVersion))
      .pipe(gulp.dest("./dist/" + newVersionNo + "/scripts/"));

    gulp.src(["./app/scripts/app.controller.js"])
      .pipe(replace(footerOldVersion, footerNewVersion))
      .pipe(gulp.dest("./app/scripts/"));
});


// Clean
gulp.task("clean", function (cb) {
    del(["dist/styles", "dist/scripts", "dist/images",  "dist", ".temp"], cb);
    console.log("clean task finished");
});


// Watch
gulp.task("watch", function () {

    // Watch .css files
    gulp.watch("./app/styles/**/*.css", ["styles"]);

    // Watch .js files
    gulp.watch("./app/scripts/**/*.config.js", ["scripts"]);
    gulp.watch("./app/scripts/**/*.directive.js", ["scripts"]);
    gulp.watch("./app/scripts/**/*.service.js", ["scripts"]);
    gulp.watch("./app/scripts/**/*.controller.js", ["scripts"]);

    // Watch .html files
    gulp.watch("./app/**/*.html", ["htmls"]);

    // Watch images files
    gulp.watch("./app/images/**/*", ["images"]);

    // Create LiveReload server
    livereload.listen();

    // Watch any files in dist/, reload on change
    gulp.watch(["./dist/**"]).on("change", livereload.changed);

});

gulp.task("command", function() {
    console.log("clean");
    console.log("default");
    console.log("urls");
    console.log("baseurl");
    console.log("index");
    console.log("footer");
});

// Default task
gulp.task("default", ["scripts", "styles", "images", "htmls", "replace-templateurl", "replace-serverurl", "replace-index", "replace-footer-version"]);

gulp.task("urls", ["replace-templateurl"]);
gulp.task("baseurl", ["replace-serverurl"]);
gulp.task("index", ["replace-index"]);
gulp.task("footer", ["replace-footer-version"]);

