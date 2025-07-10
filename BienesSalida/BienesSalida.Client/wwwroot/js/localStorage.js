export function setItem(key, value) {
    localStorage.setItem(key,value);
}
export function getItem(key) {
    return localStorage.getItem(key);
}
export function removeItem(key) {
    localStorage.removeItem(key);
}

/*
window.localStorageHelper = {
    setItem: function (key, value) {
        localStorage.setItem(key, value);
    },
    getItem: function (key) {
        return localStorage.getItem(key);
    },
    removeItem: function (key) {
        localStorage.removeItem(key);
    }
};*/