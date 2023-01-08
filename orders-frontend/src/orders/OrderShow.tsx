import {
    ShowContextProvider,
    ShowView,
    Tab,
    TabbedShowLayout,
    TextField,
    useShowController,
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
