import portugueseMessages from 'ra-language-portuguese';

export const messages = {
    simple: {
        action: {
            close: 'Fechar',
            resetViews: 'Limpar',
        },
        'create-order': 'Novo Pedido',
    },
    ...portugueseMessages,
    resources: {
        orders: {
            name: 'Pedido |||| Pedido',
            fields: {
                id: 'Código do Pedido',
                customerName: 'Nome do Cliente',
                dateCreated: 'Data de Criação',
                dateUpdated: 'Data de Atualização',
                orderTypeName: 'Tipo de Pedido',
                userName: 'Usuário',
            },
        }
    },
    order: {
        list: {
            search: 'Buscar',
        },
        form: {
            summary: 'Resumo'
        },
        edit: {
            title: 'Pedidos',
        },
        action: {
            save_and_edit: 'Salvar e Alterar',
            save_and_add: 'Salvar e Adicionar',
            save_and_show: 'Salvar E Visualizar',
        },
    }    
};

export default messages;
