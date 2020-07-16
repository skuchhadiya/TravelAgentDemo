export function deepClone<T>(object: T): T {
    if (!object) return undefined;
    return JSON.parse(JSON.stringify(object));
}
