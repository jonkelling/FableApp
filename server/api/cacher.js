import uuidv4 from 'uuid/v4';

const cache = {};

export default (fn) => {
    const key = uuidv4();
    return async () => {
        try {
            if (!cache[key]) cache[key] = await fn();
            return cache[key];
        }
        catch (err) {
            return err;
        }
    }
}