import * as React from 'react';
import { useTranslate, useRecordContext } from 'react-admin';

export default () => {
    const translate = useTranslate();
    const record = useRecordContext();
    return (
        <>
            {record
                ? translate('order.edit.title', { title: record.customerName + ' ' + record.ordertTypeName })
                : ''}
        </>
    );
};
