CREATE OR REPLACE FUNCTION get_general_metrics(
    p_currency_type TEXT,
    p_payment_method INT,
    p_from_date TIMESTAMP,
    p_to_date TIMESTAMP
)
RETURNS TABLE (
    total_clients BIGINT,
    expired_installments BIGINT,
    active_loans BIGINT,
    total_installments BIGINT,
    total_charged NUMERIC,
    total_loans_amount NUMERIC
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        (SELECT COUNT(id) FROM clients) AS totalClients,
        (SELECT COUNT(id) FROM installments WHERE status = 2) AS expiredInstallments,
        (SELECT COUNT(id) FROM loans WHERE status = 2) AS activeLoans,
        (SELECT COUNT(id) FROM installments) AS totalInstallments,
        (
            SELECT 
                COALESCE(SUM(amount_total),0) + COALESCE(SUM(surcharge_total),0)
            FROM payments
            WHERE amount_currency_type = p_currency_type
              AND payment_method = p_payment_method
              AND payment_date >= p_from_date
              AND payment_date <= p_to_date
        ) AS total_charged,
        (SELECT COALESCE(SUM(amount_total),0) FROM loans WHERE amount_currency_type = p_currency_type) AS total_loans_amount;
END;
$$ LANGUAGE plpgsql;