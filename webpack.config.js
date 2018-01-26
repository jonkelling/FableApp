const path = require("path");
const fs = require("fs");
const webpack = require("webpack");
const fableUtils = require("fable-utils");
const ExtractTextPlugin = require('extract-text-webpack-plugin');

var out_path = path.resolve("./public");
var proj_path = "./src";

const isProduction = false;

var babelOptions = fableUtils.resolveBabelOptions({
  presets: ["react", ["es2015", { "modules": false }]],
  plugins: ["transform-runtime"]
});

var cfg = {
  devtool: "source-map",
  entry:  [
    proj_path+"/FableApp.fsproj"
  ],
  output: {
      publicPath: "/",
      path: out_path,
      filename: "bundle.js"
  },
  devServer: {
    contentBase: path.resolve('./public'),
    port: 8080,
    hot: true,
    inline: true
  },
  module: {
    rules: [
      {
        test: /\.fs(x|proj)?$/,
        use: {
          loader: "fable-loader",
          options: { babel: babelOptions }
        }
      },      
      {
        test: /\.js$/,
        exclude: /node_modules[\\\/](?!fable-)/,
        use: {
          loader: 'babel-loader',
          options: babelOptions
        },
      },
      {
        test: /(\.scss|\.css)$/,
        use: ExtractTextPlugin.extract({
          fallback: 'style-loader',
          use: [
            {
              loader: 'css-loader',
              query: {
                modules: true,
                sourceMap: true,
                importLoaders: true,
                localIdentName: '[name]__[local]___[hash:base64:5]'
              }
            },
            {
              loader: 'sass-loader',
              query: {
                data: '@import "'+proj_path+'/theme/_config.scss";',
                includePaths: [path.resolve(__dirname, proj_path+'theme')]
              }
            }
          ]
        }),
      },
      {
        test: /\.(png|jpg)$/,
        use: 'url-loader?limit=8192'
      }
    ],
  },
  resolve: {
    modules: ["node_modules", path.resolve('node_modules')]
  },
  plugins: [
    new ExtractTextPlugin({ filename: 'bundle.css', allChunks: true }),
    ...(isProduction ? [] : [
        new webpack.HotModuleReplacementPlugin(),
        new webpack.NamedModulesPlugin()
    ])
  ]
};


module.exports = cfg;