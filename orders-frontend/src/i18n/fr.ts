import frenchMessages from 'ra-language-french';

export default {
    simple: {
        action: {
            close: 'Fermer',
            resetViews: 'Réinitialiser les vues',
        },
        'create-order': 'Nouvelle Pédido',
    },
    ...frenchMessages,
    resources: {
        orders: {
            name: 'Pédido |||| Pédido',
            fields: {
                id: 'Numéro de Pédido',
                customerName: 'Customer Name',
                orderTypeName: 'Taper de Pédido',
                dateCreated: 'Date de création',
                dateUpdated: 'Date de mise à jour',
                userName: 'Utilisatrice',
            },
        },        
        users: {
            name: 'Utilisatrice |||| Utilisatrice',
            fields: {
                name: 'Nom',
                role: 'Rôle',
            },
        },
    },
    order: {
        list: {
            search: 'Rechercher',
        },
        form: {
            summary: 'Résumé'
        },
        edit: {
            title: 'Pédido',
        },
        action: {
            save_and_edit: 'Enregistrer et modifier',
            save_and_add: 'Enregistrer et ajouter',
            save_and_show: 'Enregistrer et afficher',
            save_with_average_note: 'Enregistrer avec note',
        },
    }
    
};
