/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

/*==============================================================================================================================
| DEPENDENCIES
\-----------------------------------------------------------------------------------------------------------------------------*/
const   {src, dest, parallel}   = require('gulp');

const   gulpif                  = require('gulp-if');

const   sass                    = require('gulp-sass'),
        postCss                 = require("gulp-postcss"),
        autoPrefixer            = require("autoprefixer"),
        cssNano                 = require("cssnano"),
        sourceMaps              = require("gulp-sourcemaps");

/*==============================================================================================================================
| VARIABLES
\-----------------------------------------------------------------------------------------------------------------------------*/
var     environment             = 'development',
        outputDir               = '',
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
const   files                   = {
          scssPath:               'Shared/Styles/Style.scss',
          scssViewsPath:          'Shared/Styles/Views/*.scss',
          fontsPath:              [ 'node_modules/@fortawesome/fontawesome-free/webfonts/*'
                                  ]
                                }

/*==============================================================================================================================
| SET ENVIRONMENT
>-------------------------------------------------------------------------------------------------------------------------------
| Looks for an environment variable and conditionally set local context accordingly.
\-----------------------------------------------------------------------------------------------------------------------------*/
environment                     = process.env.BUILD_ENVIRONMENT || environment;

// Environment: Development
if (environment === 'development') {
  outputDir                     = 'wwwroot/';
  sassStyle                     = 'expanded';
  isProduction                  = false;
}

// Environment: Production
else {
  outputDir                     = 'wwwroot/';
  sassStyle                     = 'compressed';
  isProduction                  = true;
}

/*==============================================================================================================================
| TASK: SCSS FILE
>-------------------------------------------------------------------------------------------------------------------------------
| Compiles the SCSS  file and moves it to the build directory.
\-----------------------------------------------------------------------------------------------------------------------------*/
function scssTask() {
  return src(files.scssPath)
    .pipe(sourceMaps.init())
    .pipe(sass())
    .on("error", sass.logError)
    .pipe(postCss([
      autoPrefixer(),
      cssNano()
    ]))
    .pipe(sourceMaps.write('.'))
    .pipe(dest(outputDir + 'Shared/Styles/'));
}

/*==============================================================================================================================
| TASK: SCSS VIEWS
>-------------------------------------------------------------------------------------------------------------------------------
| Compiles SCSS view files and moves them to the build directory.
\-----------------------------------------------------------------------------------------------------------------------------*/
function scssViewsTask() {
  return src(files.scssViewsPath)
    .pipe(sourceMaps.init())
    .pipe(sass())
    .on("error", sass.logError)
    .pipe(postCss([
      autoPrefixer(),
      cssNano()
    ]))
    .pipe(sourceMaps.write('.'))
    .pipe(dest(outputDir + 'Shared/Styles/Views/'));
}

/*==============================================================================================================================
| TASK: FONTS
>-------------------------------------------------------------------------------------------------------------------------------
| Copies fonts from package manager to the build directory.
\-----------------------------------------------------------------------------------------------------------------------------*/
function fontsTask() {
  return src(files.fontsPath)
    .pipe(dest(outputDir + 'Shared/Fonts/'));
}

/*==============================================================================================================================
| EXPORT TASKS
>-------------------------------------------------------------------------------------------------------------------------------
| Exports the above defined tasks for use by gulp.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.scss                    = parallel(scssTask, scssViewsTask);

/*==============================================================================================================================
| TASK: BUILD
>-------------------------------------------------------------------------------------------------------------------------------
| Composite task that will call all build-related tasks.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.build = parallel(scssTask, scssViewsTask, fontsTask);

/*==============================================================================================================================
| TASK: DEFAULT
>-------------------------------------------------------------------------------------------------------------------------------
| The default task when Gulp runs, assuming no task is specified. Assuming the environment variable isn't explicitly defined
| otherwise, will run on development-oriented tasks.
\-----------------------------------------------------------------------------------------------------------------------------*/
exports.default = parallel(scssTask, scssViewsTask, fontsTask);
