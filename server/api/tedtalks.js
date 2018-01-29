import csvhelper from './csvhelper';
import path from 'path';
import cacher from './cacher';
import withIds from './withIds';

const basePath = path.join(__dirname, '..', '..', 'public', 'ted-talks');

export default cacher(async () => {
    const tedmain = csvhelper(path.join(basePath, 'ted_main.csv'));
    const transcripts = csvhelper(path.join(basePath, 'transcripts.csv'));
    return {
        ted: await (async () => fixData(await tedmain))(),
        transcripts: await (async () => await transcripts)()
    };
});

function fixData(data) {
    return data.map(row => {
        return {
            ...row,
            ratings: row.ratings && tryParse(row.ratings),
            related_talks: row.related_talks && tryParse(row.related_talks),
            tags: row.tags && tryParse(row.tags),
        };
    });
}

function tryParse(s) {
    try {
        return eval(s);
    } catch (error) {
        console.log(error);
        console.log(s);
        return s;
    }
}