import { test, expect } from '@playwright/test';
import { Page } from '../pages/page-model';

test.describe('A set of examples that illustrate how to use Playwright API', () => {
  let page: Page;

  test.beforeEach(async ({ page: playwrightPage }) => {
    page = new Page(playwrightPage);
    await playwrightPage.goto('https://devexpress.github.io/testcafe/example/');
  });

  test('Text typing basics', async () => {
    await page.nameInput.fill('Peter');
    await page.nameInput.fill('Paker');
    await page.nameInput.press('ArrowLeft');
    await page.nameInput.type('r');
    await expect(page.nameInput).toHaveValue('Parker');
  });

  test('Click an array of labels and then check their states', async () => {
    for (const feature of page.featureList) {
      await feature.label.click();
      await expect(feature.checkbox).toBeChecked();
    }
  });

  test('Dealing with text using keyboard', async () => {
    await page.nameInput.fill('Peter Parker');
    await page.nameInput.click({ position: { x: 5, y: 0 } });
    await page.nameInput.press('Backspace');
    await expect(page.nameInput).toHaveValue('Pete Parker');
    await page.nameInput.press('Home');
    await page.nameInput.press('ArrowRight');
    await page.nameInput.type('.');
    await page.nameInput.press('Delete');
    await page.nameInput.press('Delete');
    await page.nameInput.press('Delete');
    await expect(page.nameInput).toHaveValue('P. Parker');
  });

  test('Moving the slider', async () => {
    const initialOffset = await page.slider.handle.evaluate(el => el.offsetLeft);
    await page.triedTestCafeCheckbox.click();
    await page.slider.handle.dragTo(page.slider.tick.withText('9'));
    await expect(page.slider.handle.evaluate(el => el.offsetLeft)).toBeGreaterThan(initialOffset);
  });

  test('Dealing with text using selection', async () => {
    await page.nameInput.fill('Test Cafe');
    await page.nameInput.selectText({ start: 1, end: 7 });
    await page.nameInput.press('Delete');
    await expect(page.nameInput).toHaveValue('Tfe');
  });

  test('Handle native confirmation dialog', async ({ page: playwrightPage }) => {
    playwrightPage.on('dialog', dialog => dialog.accept());
    await page.populateButton.click();
    const dialogHistory = await playwrightPage.evaluate(() => window.confirmationDialogHistory);
    await expect(dialogHistory[0]).toBe('Reset information before proceeding?');
    await page.submitButton.click();
    await expect(page.results).toContainText('Peter Parker');
  });

  test('Pick option from select', async () => {
    await page.interfaceSelect.click();
    await page.interfaceSelectOption.withText('Both').click();
    await expect(page.interfaceSelect).toHaveValue('Both');
  });

  test('Filling a form', async () => {
    await page.nameInput.fill('Bruce Wayne');
    await page.macOSRadioButton.click();
    await page.triedTestCafeCheckbox.click();
    await page.commentsTextArea.fill("It's...");
    await page.commentsTextArea.waitForTimeout(500);
    await page.commentsTextArea.type('\ngood');
    await page.commentsTextArea.waitForTimeout(500);
    await page.commentsTextArea.selectText();
    await page.commentsTextArea.press('Delete');
    await page.commentsTextArea.fill('awesome!!!');
    await page.submitButton.click();
    await expect(page.results).toContainText('Bruce Wayne');
  });
});
