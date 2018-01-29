import fs from 'fs';
import path from 'path';

import express from 'express';
import bodyParser from 'body-parser';

import Enumerable from 'linq';

import tedtalks from './api/tedtalks';

import cors from 'cors';

var app = express()
app.use(cors());


app.set('port', (process.env.PORT || 3000))

try {
  fs.statSync('dist')
  console.log('Serving static build from dist/')
  console.log('Run `npm run clean` to return to development mode')
  app.use('/', express.static(path.join(__dirname, 'dist')));
}
catch (e) {
  console.log('Serving development build with nwb middleware')
  console.log('Run `npm run build` to create a production build')
  // app.use(require('nwb/express')(express))
}

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}));

// Additional middleware which will set headers that we need on each request.
app.use(function(req, res, next) {
    // Disable caching so we'll always get the latest comments.
    res.setHeader('Cache-Control', 'no-cache')
    next()
});

app.get('/api/tedtalks', async function(req, res) {
  res.json(await tedtalks());
})

app.get('/api/museums', async function(req, res) {
  res.json(await museums());
})

app.listen(app.get('port'), function() {
  console.log('Server started: http://localhost:' + app.get('port') + '/')
});