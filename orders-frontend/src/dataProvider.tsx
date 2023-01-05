import simpleRestProvider from 'ra-data-simple-rest';
import { fetchUtils, combineDataProviders } from 'react-admin';

const URL_API = process.env.URL_API;

const httpClient = (url: any, options: any = {}) => {    
    const { token } = JSON.parse(localStorage.getItem('auth') as any);
    options.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8', 'Authorization': `Bearer ${token}` })
    const queryParams = new URLSearchParams(url)
    const filter = queryParams.get("filter")?.split(":")[1];
    const sortArray = JSON.parse(queryParams.get("sort") as string);
    const sort = sortArray[0];
    const order = sortArray[1];
    const range = JSON.parse(queryParams.get("range") as string);
    const start = range[0];
    const size = range[1];
    const urlPrefix = url.split("?")[0];
    const newUrl = `${urlPrefix}?filter=${filter}&sort=${sort}&order=${order}&start=${start}&size=${size}`;
    console.log({ newUrl });
    return fetchUtils.fetchJson(newUrl, options);
};


const orderProvider = simpleRestProvider(`${ URL_API }Order`, httpClient);
const orderTypeProvider = simpleRestProvider(`${ URL_API }OrderType`, httpClient);
const userProvider = simpleRestProvider(`${ URL_API }UserS`, httpClient);

const dataProvider = combineDataProviders((resource) => {
    switch (resource) {
        case 'orders':
            return orderProvider;
        case 'users':
            return userProvider;
        case 'orderTypes':
            return orderTypeProvider;
        default:
            throw new Error(`Unknown resource: ${resource}`);
    }
});
export default dataProvider;
