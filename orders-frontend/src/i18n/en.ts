import englishMessages from 'ra-language-english';

export const messages = {
    simple: {
        action: {
            close: 'Close',
            resetViews: 'Reset views',
        },
        'create-order': 'New Order',
    },
    ...englishMessages,
    resources: {
        orders: {
            name: 'Order |||| Order',
            fields: {
                id: 'Order ID',
                customerName: 'Customer Name',
                dateCreated: 'Create Date',
            },
        },        
        users: {
            name: 'User |||| Users',
            fields: {
                name: 'Name',
                role: 'Role',
            },
        },
    },
    order: {
        list: {
            search: 'Search',
        },
        form: {
            summary: 'Summary'
        },
        edit: {
            title: 'Orders "%{customerName}"',
        },
        action: {
            save_and_edit: 'Save and Edit',
            save_and_add: 'Save and Add',
            save_and_show: 'Save and Show',
            save_with_average_note: 'Save with Note',
        },
    },
    user: {
        list: {
            search: 'Search',
        },
        form: {
            summary: 'Summary',
            security: 'Security',
        },
        action: {
            save_and_add: 'Save and Add',
            save_and_show: 'Save and Show',
        },
    },
};

export default messages;
