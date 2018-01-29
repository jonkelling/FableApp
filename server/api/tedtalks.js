import csvhelper from './csvhelper';
import path from 'path';
import cacher from './cacher';
import withIds from './withIds';

const basePath = path.join(__dirname, '..', '..', 'public', 'ted-talks');

export default cacher(async () => {
    const tedmain = csvhelper(path.join(basePath, 'ted_main.csv'));
    const transcripts = csvhelper(path.join(basePath, 'transcripts.csv'));
    return {
        ted: await (async () => await tedmain)(),
        transcripts: await (async () => await transcripts)()
    };
});