import * as React from 'react';
import { useMemo } from 'react';
import {
    Create,
    required,
    ReferenceInput,
    SaveButton,
    SelectInput,
    SimpleFormConfigurable,
    TextInput,
    Toolbar,
    useNotify,
    usePermissions,
    useRedirect,
    useCreate,
    useCreateSuggestionContext,
} from 'react-admin';
import { useFormContext, useWatch } from 'react-hook-form';
import { Button, Dialog, DialogActions, DialogContent } from '@mui/material';

const OrderCreateToolbar = () => {
    const notify = useNotify();
    const redirect = useRedirect();
    const { reset } = useFormContext();

    return (
        <Toolbar>
            <SaveButton label="order.action.save_and_edit" variant="text" />
            <SaveButton
                label="order.action.save_and_show"
                type="button"
                variant="text"
                mutationOptions={{
                    onSuccess: data => {
                        notify('ra.notification.created', {
                            type: 'info',
                            messageArgs: { smart_count: 1 },
                        });
                        redirect('show', 'orders', data.id);
                    },
                }}
            />
            <SaveButton
                label="order.action.save_and_add"
                type="button"
                variant="text"
                mutationOptions={{
                    onSuccess: () => {
                        reset();
                        window.scrollTo(0, 0);
                        notify('ra.notification.created', {
                            type: 'info',
                            messageArgs: { smart_count: 1 },
                        });
                    },
                }}
            />            
        </Toolbar>
    );
};

const OrderCreate = () => {
          
   
    const { permissions } = usePermissions();
    const dateDefaultValue = useMemo(() => new Date(), []);
    return (
        <Create redirect="edit">
            <SimpleFormConfigurable
                toolbar={<OrderCreateToolbar />}
            >
                <ReferenceInput label="Order Type" source="orderTypeId" reference="OrderType">
                    <SelectInput source='id' optionText="type" validate={required("Required field")}  />
                </ReferenceInput>
                
                <TextInput
                    source="customerName"
                    fullWidth
                    multiline
                    validate={required('Required field')}
                />                
            </SimpleFormConfigurable>
        </Create>
    );
};

export default OrderCreate;

const DependantInput = ({
    dependency,
    children,
}: {
    dependency: string;
    children: JSX.Element;
}) => {
    const dependencyValue = useWatch({ name: dependency });

    return dependencyValue ? children : null;
};

const CreateUser = () => {
    const { filter, onCancel, onCreate } = useCreateSuggestionContext();
    const [value, setValue] = React.useState(filter || '');
    const [create] = useCreate();

    const handleSubmit = ({event} : any) => {
        event.preventDefault();
        create(
            'users',
            {
                data: {
                    name: value,
                },
            },
            {
                onSuccess: data => {
                    setValue('');
                    onCreate(data);
                },
            }
        );
    };

    return (
        <Dialog open onClose={onCancel}>
            <form onSubmit={handleSubmit}>
                <DialogContent>
                    <TextInput
                        source="customerName"
                        defaultValue="RED Technologies"
                        autoFocus
                        validate={[required()]}
                    />
                    
                </DialogContent>
                <DialogActions>
                    <Button type="submit">Save</Button>
                    <Button onClick={onCancel}>Cancel</Button>
                </DialogActions>
            </form>
        </Dialog>
    );
};
