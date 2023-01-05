import frenchMessages from 'ra-language-french';

export default {
    simple: {
        action: {
            close: 'Close',
            resetViews: 'Reset views',
        },
        'create-order': 'New Order',
    },
    ...frenchMessages,
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
