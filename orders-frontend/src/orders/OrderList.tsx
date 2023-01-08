import { Fragment, memo } from 'react';
import BookIcon from '@mui/icons-material/Book';
import { Box, Chip, useMediaQuery } from '@mui/material';
import { Theme, styled } from '@mui/material/styles';
import jsonExport from 'jsonexport/dist';
import {
    BulkDeleteButton,
    BulkExportButton,
    SelectColumnsButton,
    CreateButton,
    DatagridConfigurable,
    DateField,
    downloadCSV,
    EditButton,
    ExportButton,
    FilterButton,
    List,
    SearchInput,
    ShowButton,
    SimpleList,
    TextField,
    TopToolbar,
    useTranslate,
    ReferenceInput,
    SelectInput,
    required,
} from 'react-admin'; // eslint-disable-line import/no-unresolved

export const OrderIcon = BookIcon;

const QuickFilter = ({ label, source, defaultValue }: any) => {
    const translate = useTranslate();
    return <Chip sx={{ marginBottom: 1 }} label={translate(label)} />;
};

const orderFilter = [
    <SearchInput source="customerName" alwaysOn />,
    <ReferenceInput source="orderTypeId" reference="OrderType" alwaysOn={true}>
        <SelectInput label="resources.orders.fields.orderTypeName" source='id' optionText="type" defaultValue='2D9D8CF3-80E7-4E0A-B6A4-544B5F169DB5' emptyText="Select..." />
    </ReferenceInput>,    
    <QuickFilter
        label="resources.orders.fields.customerName"
        source="customerName"
    />,
];

const exporter = (Orders: any) => {
    const ordersExport = Orders.map(({order}: any) => {
        const { id, userId, orderTypeId, ...orderForExport } = order; // omit backlinks and author
        return orderForExport;
    });
    console.log(ordersExport);
    // change the rowDelimiter to change the CSV file delimiter
    return jsonExport(ordersExport, {rowDelimiter: ';'}, (err: any, csv: any) => downloadCSV(csv, 'Order'));
};

const StyledDatagrid = styled(DatagridConfigurable)(({ theme }) => ({
    '& .title': {
        maxWidth: '20em',
        overflow: 'hidden',
        textOverflow: 'ellipsis',
        whiteSpace: 'nowrap',
    },
    '& .hiddenOnSmallScreens': {
        [theme.breakpoints.down('lg')]: {
            display: 'none',
        },
    },
    '& .column-tags': {
        minWidth: '9em',
    },
    '& .publishedAt': { fontStyle: 'italic' },
}));

const OrderListBulkActions = memo(({ children, ...props }: any) => (
    <Fragment>
        <BulkDeleteButton {...props} />
        <BulkExportButton {...props} />
    </Fragment>
));

const OrderListActions = () => (
    <TopToolbar>
        <SelectColumnsButton />
        <FilterButton />
        <CreateButton />
        <ExportButton />
    </TopToolbar>
);

const OrderListActionToolbar = ({ children, ...props }: any) => (
    <Box sx={{ alignItems: 'center', display: 'flex' }}>{children}</Box>
);

const rowClick = ({id, resource, record}: any) => {
    if (id) {
        return 'edit';
    }

    return 'show';
};

const OrderList = () => {
    const isSmall = useMediaQuery<Theme>(theme => theme.breakpoints.down('md'));
    return (
        <List
            filters={orderFilter}
            sort={{ field: 'dateCreated', order: 'DESC' }}
            exporter={exporter}
            actions={<OrderListActions />}
        >
            {isSmall ? (
                <SimpleList
                    primaryText={record => record.id}
                    secondaryText={record => record.publishedAt}
                    tertiaryText={record =>
                        new Date(record.dateCreated).toLocaleDateString()
                    }
                />
            ) : (
                <StyledDatagrid
                    bulkActionButtons={<OrderListBulkActions />}
                    rowClick={rowClick}
                >
                    <TextField label="resources.orders.fields.id" source="id" />
                    <TextField label="resources.orders.fields.orderTypeName" source="orderTypeName" cellClassName="title" />
                    <TextField label="resources.orders.fields.customerName" source="customerName" cellClassName="title" />
                    <TextField label="resources.orders.fields.userName" source="userName" cellClassName="title" />
                    <DateField
                        label="resources.orders.fields.dateCreated"
                        source="dateCreated"
                        sortByOrder="DESC"
                        cellClassName="publishedAt"
                    />
                    <DateField
                        label="resources.orders.fields.dateUpdated"
                        source="dateUpdated"
                        sortByOrder="DESC"
                        cellClassName="publishedAt"
                    />
                    <OrderListActionToolbar>
                        <EditButton />
                        <ShowButton />
                    </OrderListActionToolbar>
                </StyledDatagrid>
            )}
        </List>
    );
};

const tagSort = { field: 'name.en', order: 'ASC' };

export default OrderList;
