import {
    TopToolbar,    
    Edit,
    CloneButton,
    CreateButton,
    ShowButton,
    FormTab,
   
    ReferenceInput,
    SelectInput,
    TabbedForm,
    TextInput,
    required,
    EditActionsProps,
} from 'react-admin'; // eslint-disable-line import/no-unresolved
import {
    Box,
    BoxProps,
} from '@mui/material';
import OrderTitle from './OrderTitle';

const EditActions = ({ hasShow }: EditActionsProps) => (
    <TopToolbar>
        <CloneButton className="button-clone" />
        {hasShow && <ShowButton />}
        {/* FIXME: added because react-router HashHistory cannot block navigation induced by address bar changes */}
        <CreateButton />
    </TopToolbar>
);

const SanitizedBox = ({
    fullWidth,
    ...props
}: BoxProps & { fullWidth?: boolean }) => <Box {...props} />;

const OrderEdit = () => {
    return (
        <Edit title={<OrderTitle />} actions={<EditActions />}>
            <TabbedForm
                defaultValues={{ average_note: 0 }}
                warnWhenUnsavedChanges
            >
                <FormTab label="order.form.summary">
                    <SanitizedBox
                        display="flex"
                        flexDirection="column"
                        width="100%"
                        justifyContent="space-between"
                        fullWidth
                    >
                        <TextInput disabled source="id" />
                        
                    </SanitizedBox>
                    <ReferenceInput label="Order Type" source="orderTypeId" reference="OrderType">
                        <SelectInput source='id' optionText="type" validate={required("Required field")}  />
                    </ReferenceInput>
                    
                    <TextInput
                        source="customerName"
                        fullWidth
                        multiline
                        validate={required('Required field')}
                    />                 
                    
                </FormTab>
            </TabbedForm>
        </Edit>
    );
};

export default OrderEdit;
