import spanishMessages from 'ra-language-spanish';

export const messages = {
    simple: {
        action: {
            close: 'Cerca',
            resetViews: 'Limpiar',
        },
        'create-order': 'Nuevo Pedido',
    },
    ...spanishMessages,
    resources: {
        orders: {
            name: 'Pedido |||| Pedido',
            fields: {
                id: 'Order ID',
                customerName: 'Nombre del cliente',
                dateCreated: 'Fecha de Creación',
                dateUpdated: 'Fecha de Actualización',
                orderTypeName: 'Tipo de Pedido',
                userName: 'Usuario', 
            },
        }
    },
    order: {
        list: {
            search: 'Buscar',
        },
        form: {
            summary: 'Resumen'
        },
        edit: {
            title: 'Pedido',
        },
        action: {
            save_and_edit: 'Guardar y editar',
            save_and_add: 'Guardary y añadir',
            save_and_show: 'Guardar y mostrar',
        },
    },
    ra: {
        action: {
            search: 'Buscar',
            show: 'Mostrar',
            list: 'Lista',
            save: 'Guardar',
            create: 'Crear',
            edit: 'Editar',
            delete: 'Borrar',
            cancel: 'Cancelar',
            refresh: 'Actualizar',
            add_filter: 'Añadir filtro',
            remove_filter: 'Eliminar filtro',
            back: 'Volver',
            bulk_actions: '1 elemento seleccionado |||| %{smart_count} elementos seleccionados',
            export: 'Exportar',
            clear_input_value: 'Limpiar valor',
            clone: 'Clonar',
            unselect: 'Deseleccionar',
            expand: 'Expandir',
            close: 'Cerrar',
            open_menu: 'Abrir menú'
        },
        navigation: {
            no_results: 'No se han encontrado resultados',
            no_more_results: 'La página %{page} está fuera de los límites. Prueba la página anterior.',
            page_out_of_boundaries: 'La página %{page} está fuera de los límites',
            page_rows_per_page: 'Filas por página:',
            page_out_from_end: 'No se puede ir más allá de la última página',
            page_out_from_begin: 'No se puede ir antes de la primera página',
            page_range_info: '%{offsetBegin}-%{offsetEnd} de %{total}',
            next: 'Siguiente',
            prev: 'Anterior'
        },
        auth: {
            username: 'Usuario',
            password: 'Contraseña',
            sign_in: 'Iniciar sesión',
            sign_in_error: 'La autenticación ha fallado, inténtelo de nuevo',
            logout: 'Cerrar sesión'
        },
        page: {
            list: 'Lista de %{name}',
            edit: '%{name} #%{id}',
            show: '%{name} #%{id}', 
            create: 'Crear %{name}',
            dashboard: 'Panel de control',
            not_found: 'No encontrado',
            loading: 'Cargando',
            empty: 'No hay %{name} todavía.',
            invite: '¿Quieres añadir uno?'
        },
    }
};

export default messages;
