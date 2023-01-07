import * as React from 'react';
import {
    ArrayField,
    BooleanField,
    CloneButton,
    ChipField,
    Datagrid,
    DateField,
    EditButton,
    NumberField,
    ReferenceArrayField,
    ReferenceManyField,
    RichTextField,
    SelectField,
    ShowContextProvider,
    ShowView,
    SingleFieldList,
    Tab,
    TabbedShowLayout,
    TextField,
    UrlField,
    useShowController,
    useLocaleState,
    useRecordContext,
} from 'react-admin';
import OrderTitle from './OrderTitle';

const OrderShow = () => {
    const controllerProps = useShowController();
    return (
        <ShowContextProvider value={controllerProps}>
            <ShowView title={<OrderTitle />}>
                <TabbedShowLayout>
                    <Tab label="order.form.summary">
                        <TextField source="id" />
                        <TextField source="orderTypeName" cellClassName="title" />
                    <TextField source="customerName" cellClassName="title" />
                    <TextField source="userName" cellClassName="title" />
                    </Tab>
                </TabbedShowLayout>
            </ShowView>
        </ShowContextProvider>
    );
};

export default OrderShow;
