import csv from 'csv';
import fs from 'fs';

async function readFile(filename) {
    return await new Promise(resolve => {
        fs.readFile(filename, function (err, data) {
            if (err) throw err;
            resolve(data);
        });
    });
}

async function getData(filename) {
    return await new Promise(async resolve => {
        const fileData = await readFile(`${filename}`);
        csv.parse(fileData, {auto_parse: true, columns: true, trim: true}, (err, output) => {
            if (err) throw err;
            resolve(output);
        });
    });
}

export default async filename => await getData(filename);