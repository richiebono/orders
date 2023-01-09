import simpleRestProvider from 'ra-data-simple-rest';
import { fetchUtils, combineDataProviders } from 'react-admin';

const URL_API = process.env.URL_API;

const httpClient = (url: any, options: any = {}) => {  

    const { token } = JSON.parse(localStorage.getItem('auth') as any);
    options.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8', 'Authorization': `Bearer ${token}` })
    var newUrl = url;

    if(url.includes("filter")) {

        const urlPrefix = url.split("?")[0];
        const urlSuffix = url.split("?")[1];
        newUrl = `${urlPrefix}?`;
        const queryParams = new URLSearchParams(urlSuffix)
        const filter = queryParams.get("filter");
        
        if(filter) {
            const newFilter = JSON.parse(filter);
            const customerName = newFilter.customerName;
            const orderTypeId = newFilter.orderTypeId;
            if(customerName) newUrl += `search=${customerName}`;
            if(orderTypeId) newUrl += `type=${orderTypeId}`;
        }
        
        const sortArray = JSON.parse(queryParams.get("sort") as string);
        if(sortArray && sortArray.length > 0) {
            const sort = sortArray[0];
            const order = sortArray[1];
            if(filter) {
                newUrl += `&sort=${sort}&order=${order}`;
            }
            else{
                newUrl += `sort=${sort}&order=${order}`;
            }
        }
        
        const range = JSON.parse(queryParams.get("range") as string);
        if(range && range.length > 0) {
            const start = range[0];
            const size = range[1];
            if(filter || sortArray.length > 0)
            {
                newUrl += `&start=${start}&size=${size}`;
            }
            else{
                newUrl += `start=${start}&size=${size}`;
            }
        }
        
    }

    console.log({ newUrl, options });
    return fetchUtils.fetchJson(newUrl, options);
};


const orderProvider = simpleRestProvider(`${ URL_API }`, httpClient);
const orderTypeProvider = simpleRestProvider(`${ URL_API }`, httpClient);
const userProvider = simpleRestProvider(`${ URL_API }`, httpClient);

const dataProvider = combineDataProviders((resource) => {
    switch (resource) {
        case 'Order':
            return orderProvider;
        case 'User':
            return userProvider;
        case 'OrderType':
            return orderTypeProvider;
        default:
            throw new Error(`Unknown resource: ${resource}`);
    }
});
export default dataProvider;
