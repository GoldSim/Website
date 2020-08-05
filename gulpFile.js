/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/*==============================================================================================================================
| DEPENDENCIES
\-----------------------------------------------------------------------------------------------------------------------------*/
const   {src, dest, parallel}   = require('gulp');

const   gulpif                  = require('gulp-if'),
        concat                  = require('gulp-concat'),
        merge                   = require('merge2');

const   sass                    = require('gulp-sass'),
        postCss                 = require("gulp-postcss"),
        autoPrefixer            = require("autoprefixer"),
        cssNano                 = require("cssnano"),
        sourceMaps              = require("gulp-sourcemaps"),
        jshint                  = require('gulp-jshint'),
        uglify                  = require('gulp-uglify');

/*==============================================================================================================================
| VARIABLES
\-----------------------------------------------------------------------------------------------------------------------------*/
var     environment             = 'development',
        outputDir               = 'wwwroot',
        isProduction            = false;

/*==============================================================================================================================
| SOURCE FILE PATHS
>-------------------------------------------------------------------------------------------------------------------------------
| Paths to files referenced in the build process. Path names may use any "magic" glob characters, as documented at
| https://github.com/isaacs/node-glob.
>-------------------------------------------------------------------------------------------------------------------------------
| ### NOTE: JJC021715: These paths are only intended for source files. Destination files will not use glob "magic", and will
| be conditional based on the outputDir. As a result, they will likely be hardcoded into each task's dest() method.
\-----------------------------------------------------------------------------------------------------------------------------*/
const files = {
  scss                          : [ 'Shared/Styles/**/*.scss',
                                    'Shared/Styles/**/!*.scss'
                                  ],
  js                            : 'Shared/Scripts/*.js',
  jsViews                       : 'Shared/Scripts/Views/**/*.js'
}

/*==============================================================================================================================
| DEPENDENCIES
>-------------------------------------------------------------------------------------------------------------------------------
| Paths to third-party dependencies that need to by copies into the project. This is exclusively for pre-compiled client-side
| files, such as JavaScript (excluding TypeScript), CSS (excluding SCSS), images, and the occasional font.
\-----------------------------------------------------------------------------------------------------------------------------*/
const dependencies = {
  'Scripts': {
    'ApplicationInsights'       : 'node_modules/@microsoft/applicationinsights-web/dist/*.min.*',
    'DashJS'                    : 'node_modules/dashjs/dist/dash.mediaplayer.*',
    'GreenSock'                 : [ 'node_modules/gsap/src/minified/**',
                                    'node_modules/gsap/src/uncompressed/**'
                                  ],
    'Headroom'                  : 'node_modules/headroom.js/dist/**',
    'jQuery'                    : 'node_modules/jquery/dist/*.*',
    'OwlCarousel'               : 'node_modules/owl.carousel/dist/*.js',
    'ScrollMagic'               : [ 'node_modules/scrollmagic/scrollmagic/minified/**',
                                    'scrollmagic/scrollmagic/uncompressed/**'
                                  ],
    'ZURB'                      : 'node_modules/foundation-sites/dist/js/**/*.min.*',
  },
  'Styles': {
    'OwlCarousel'               : 'node_modules/owl.carousel/dist/assets/*.*'
  },
  'Fonts': {
   'FontAwesome'                : 'node_modules/@fortawesome/fontawesome-free/webfonts/*'
  }
}

/*==============================================================================================================================
| SET ENVIRONMENT
>-------------------------------------------------------------------------------------------------------------------------------
| Looks for an environment variable and conditionally set local context accordingly.
\-----------------------------------------------------------------------------------------------------------------------------*/
environment                     = process.env.BUILD_ENVIRONMENT || environment;

// Environment: Development
if (environment === 'development') {
  isProduction                  = false;
}

// Environment: Production
else {
  isProduction                  = true;
}

/*==============================================================================================================================
| TASK: SCSS
>-------------------------------------------------------------------------------------------------------------------------------
| Compiles the SCSS files, including views, and moves them to the build directory.
\-----------------------------------------------------------------------------------------------------------------------------*/
function scssTask() {
  return src(files.scss, { base: 'Shared/Styles' })
    //.pipe(autoPrefixer({ browsers: ['last 2 versions', 'safari 5', 'ie 8', 'ie 9', 'opera 12.1', 'ios 6', 'android 4'] }))
    //.pipe(sassUnicode())
    .pipe(sourceMaps.init())
    .pipe(sass({
      includePaths: [
        './Shared/Styles',
        './node_modules/foundation-sites/scss',
        './node_modules/@fortawesome'
      ]
    }))
    .on("error", sass.logError)
    .pipe(postCss([
      autoPrefixer(),
      cssNano()
    ]))
    .pipe(sourceMaps.write('.'))
    .pipe(dest(outputDir + '/Shared/Styles/'));
}

/*==============================================================================================================================
| TASK: JAVASCRIPT FILES
>-------------------------------------------------------------------------------------------------------------------------------
| Minimizes JavaScript files as part of production process.
\-----------------------------------------------------------------------------------------------------------------------------*/
function jsTask() {
  return src(files.js, { base: 'Shared/Scripts' })
    //.pipe(jshint('.jshintrc'))
    .pipe(sourceMaps.init())
    .pipe(jshint())
    .pipe(jshint.reporter('default'))
    .pipe(concat('Scripts.js'))
    .pipe(uglify())
    .pipe(sourceMaps.write('.'))
    .pipe(dest(outputDir + '/Shared/Scripts/'));
}

/*==============================================================================================================================
| TASK: JAVASCRIPT VIEWS
>-------------------------------------------------------------------------------------------------------------------------------
| Minimizes JavaScript files associated with views as part of production process.
\-----------------------------------------------------------------------------------------------------------------------------*/
function jsViewsTask() {
  return src(files.jsViews)
    .pipe(sourceMaps.init())
    .pipe(jshint())
    .pipe(jshint.reporter('default'))
    .pipe(uglify())
    .pipe(sourceMaps.write('.'))
    .pipe(dest(outputDir + '/Shared/Scripts/Views/'));
}

/*==============================================================================================================================
| TASK: DEPENDENCIES
>-------------------------------------------------------------------------------------------------------------------------------
| Copies static dependencies from their source folders and into their appropriate build folders.
\-----------------------------------------------------------------------------------------------------------------------------*/
function dependenciesTask() {
  console.log("Beginning dependencies task");
  var streams = [];
  for (var contentType in dependencies) {
    for (var dependency in dependencies[contentType]) {
      streams.push(
        src(dependencies[contentType][dependency])
          .pipe(dest(outputDir.concat('/Shared/', contentType, '/Vendor/', dependency)))
      );
    }
  }
  return merge(streams);
}

/*==============================================================================================================================
| EXPORT TASKS
>-------------------------------------------------------------------------------------------------------------------------------
| Exports the above defined tasks for use by gulp.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.scss                    = scssTask;
exports.dependencies            = dependenciesTask;
exports.js                      = parallel(jsTask, jsViewsTask);

/*==============================================================================================================================
| TASK: BUILD
>-------------------------------------------------------------------------------------------------------------------------------
| Composite task that will call all build-related tasks.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.build = parallel(scssTask, dependenciesTask, jsTask, jsViewsTask);

/*==============================================================================================================================
| TASK: DEFAULT
>-------------------------------------------------------------------------------------------------------------------------------
| The default task when Gulp runs, assuming no task is specified. Assuming the environment variable isn't explicitly defined
| otherwise, will run on development-oriented tasks.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.default = parallel(scssTask, dependenciesTask, jsTask, jsViewsTask);