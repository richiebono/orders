import * as React from 'react';
import { Fragment, memo } from 'react';
import BookIcon from '@mui/icons-material/Book';
import { Box, Chip, useMediaQuery } from '@mui/material';
import { Theme, styled } from '@mui/material/styles';
import lodashGet from 'lodash/get';
import jsonExport from 'jsonexport/dist';
import {
    BooleanField,
    BulkDeleteButton,
    BulkExportButton,
    ChipField,
    SelectColumnsButton,
    CreateButton,
    DatagridConfigurable,
    DateField,
    downloadCSV,
    EditButton,
    ExportButton,
    FilterButton,
    List,
    NumberField,
    ReferenceArrayField,
    SearchInput,
    ShowButton,
    SimpleList,
    SingleFieldList,
    TextField,
    TextInput,
    TopToolbar,
    useTranslate,
} from 'react-admin'; // eslint-disable-line import/no-unresolved

export const OrderIcon = BookIcon;

const QuickFilter = ({ label, source, defaultValue }: any) => {
    const translate = useTranslate();
    return <Chip sx={{ marginBottom: 1 }} label={translate(label)} />;
};

const orderFilter = [
    <SearchInput source="customerName" alwaysOn />,
    <QuickFilter
        label="resources.orders.fields.customerName"
        source="customerName"
        defaultValue
    />,
];

const exporter = (orders: any) => {
    const data = orders.map(({order}: any) => ({
        ...order,
    }));
    return jsonExport(data, (err: any, csv: any) => downloadCSV(csv, 'orders'));
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

const OrderPanel = ({ id, record, resource }: any) => (
    <div dangerouslySetInnerHTML={{ __html: record.body }} />
);

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
                    expand={OrderPanel}                    
                >
                    <TextField source="id" />
                    <TextField source="orderTypeName" cellClassName="title" />
                    <TextField source="customerName" cellClassName="title" />
                    <TextField source="userName" cellClassName="title" />
                    <DateField
                        source="dateCreated"
                        sortByOrder="DESC"
                        cellClassName="publishedAt"
                    />
                    <DateField
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
